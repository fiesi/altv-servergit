﻿using System.Diagnostics;

using AltV.Net;

using Npgsql;

namespace NAMERP
{
    internal class Database
    {
        private static readonly string _host = "xxx.xxx.xxx.xxx";
        private static readonly int _port = 5432;
        private static readonly string _database = "DATABASE";
        private static readonly string _username = "USERNAME";
        private static readonly string _password = "PASSWORD";

        // How many connections?
        private static readonly int _pool_size = 35;
        // Reconnect all connections after x seconds
        private static readonly int _reconnect_interval = 20;

        private static Database? _instance;

        private readonly List<NpgsqlConnection> _connections;

        private Database()
        {
            _connections = new List<NpgsqlConnection>();

            PopulatePool();
            StartReconnectThread();
        }

        public static Database GetInstance()
        {
            return _instance ??= new Database();
        }

        public static void ExecuteNonQuery(NpgsqlCommand cmd)
        {
            NpgsqlConnection conn = GetInstance().GetConnection();
            cmd.Connection = conn;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (NpgsqlException ex)
            {
                Alt.LogError($"DB ERROR: {ex.ErrorCode} | {ex.Message}");
                throw ex;
            }
            finally
            {
                GetInstance().FreeConnection(conn);
            }
        }

        public static void ExecuteScalar<T>(NpgsqlCommand cmd, out T? result)
        {
            NpgsqlConnection conn = GetInstance().GetConnection();
            cmd.Connection = conn;
            try
            {
                result = (T?)cmd.ExecuteScalar();
            }
            catch (NpgsqlException ex)
            {
                Alt.LogError($"DB ERROR: {ex.ErrorCode} | {ex.Message}");
                throw ex;
            }
            finally
            {
                GetInstance().FreeConnection(conn);
            }
        }

        public NpgsqlConnection GetConnection()
        {
            NpgsqlConnection? conn = null;

            do
            {
                Monitor.Enter(_connections);

                bool sleep = false;

                if (_connections.Count > 0)
                {
                    conn = _connections[0];
                    _connections.RemoveAt(0);
                }
                else
                {
                    sleep = true;
                }

                Monitor.Exit(_connections);

                if (sleep)
                {
                    Task.Delay(1).Wait();
                }
            }
            while (conn == null);

            return conn;
        }

        public void FreeConnection(NpgsqlConnection conn)
        {
#if DEBUG
            if (IsReaderAttached(conn))
            {
                string errorMsg = "Datenbank-Reader nicht geschlossen! Letzte Stack-Frames:\n";

                StackFrame[] frames = new StackTrace().GetFrames();
                int interestingFrameCount = Math.Min(frames.Length, 10);

                for (ushort i = 0; i < interestingFrameCount; i++)
                {
                    StackFrame frame = frames[i];
                    errorMsg += ("Index" + i).PadRight(15) + "Class=" + frame.GetMethod()?.DeclaringType?.FullName?.PadRight(40) + "Method=" + frame.GetMethod()?.Name + "\n";
                }

                Alt.LogError(errorMsg);
            }
#endif
            Monitor.Enter(_connections);
            _connections.Add(conn);
            Monitor.Exit(_connections);
        }

        private void PopulatePool()
        {
            string connString = $"Host={_host};Port={_port};Username={_username};Password={_password};Database={_database};Keepalive=1";

            for (ushort i = 0; i < _pool_size; i++)
            {
                try
                {
                    NpgsqlConnection conn = new(connString);
                    conn.Open();
                    _connections.Add(conn);
                }
                catch (Exception ex)
                {
                    Alt.LogError($"Beim Erstellen der Pool-Population ist ein Fehler aufgetreten: {ex.Message}");
                }
            }
        }

        private void StartReconnectThread()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    Task.Delay(1000 * /*60 **/ _reconnect_interval).Wait();

                    int connClosed = 0;

                    do
                    {
                        Monitor.Enter(_connections);

                        bool sleep = false;

                        if (_connections.Count > 0)
                        {
                            NpgsqlConnection conn = _connections[0];
                            _connections.RemoveAt(0);
                            conn.Close();
                            connClosed++;
                        }
                        else
                        {
                            sleep = true;
                        }

                        Monitor.Exit(_connections);

                        if (sleep)
                        {
                            Task.Delay(1).Wait();
                        }
                    }
                    while (connClosed < _pool_size);

                    Monitor.Enter(_connections);
                    PopulatePool();
                    Monitor.Exit(_connections);
                }
            });
        }

        private static bool IsReaderAttached(NpgsqlConnection conn)
        {
            try
            {
                string query = "SELECT 0;";
                using NpgsqlCommand cmd = new(query, conn);
                NpgsqlDataReader rdr = cmd.ExecuteReader();
                rdr.Close();
            }
            catch
            {
                return true;
            }

            return false;
        }
    }
}
