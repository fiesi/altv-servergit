<template>
  <main class="text-white text-sm leading-4">
    <!-- STATS -->
    <div class="absolute right-4 top-4 grid gap-y-8">
      <div class="w-[13.1vw]">
        <div class="flex justify-between text-end">
          <div class="grid">
            <p class="font-semibold opacity-75">ID</p>
            <p>{{ userId }}</p>
          </div>
          <div class="grid">
            <p class="font-semibold opacity-75">SID</p>
            <p>{{ userStaticId }}</p>
          </div>
          <div class="grid">
            <p class="font-semibold">NAMELESS.COM</p>
            <p>{{ currentDate }}, {{ currentTime }}</p>
          </div>
        </div>
      </div>

      <div class="text-end">
        <div>
          <div class="flex justify-end">
            <svg class="w-4 h-4 ms-1" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="currentColor" viewBox="0 0 22 20" :style="[wanteds > 0 ? { 'color': '#fde047' } : { 'color': '#d1d5db', 'opacity': '50%'} ]">
              <path d="M20.924 7.625a1.523 1.523 0 0 0-1.238-1.044l-5.051-.734-2.259-4.577a1.534 1.534 0 0 0-2.752 0L7.365 5.847l-5.051.734A1.535 1.535 0 0 0 1.463 9.2l3.656 3.563-.863 5.031a1.532 1.532 0 0 0 2.226 1.616L11 17.033l4.518 2.375a1.534 1.534 0 0 0 2.226-1.617l-.863-5.03L20.537 9.2a1.523 1.523 0 0 0 .387-1.575Z"/>
            </svg>
            <svg class="w-4 h-4 ms-1" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="currentColor" viewBox="0 0 22 20" :style="[wanteds > 1 ? { 'color': '#fde047' } : { 'color': '#d1d5db', 'opacity': '50%'} ]">
              <path d="M20.924 7.625a1.523 1.523 0 0 0-1.238-1.044l-5.051-.734-2.259-4.577a1.534 1.534 0 0 0-2.752 0L7.365 5.847l-5.051.734A1.535 1.535 0 0 0 1.463 9.2l3.656 3.563-.863 5.031a1.532 1.532 0 0 0 2.226 1.616L11 17.033l4.518 2.375a1.534 1.534 0 0 0 2.226-1.617l-.863-5.03L20.537 9.2a1.523 1.523 0 0 0 .387-1.575Z"/>
            </svg>
            <svg class="w-4 h-4 ms-1" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="currentColor" viewBox="0 0 22 20" :style="[wanteds > 2 ? { 'color': '#fde047' } : { 'color': '#d1d5db', 'opacity': '50%'} ]">
              <path d="M20.924 7.625a1.523 1.523 0 0 0-1.238-1.044l-5.051-.734-2.259-4.577a1.534 1.534 0 0 0-2.752 0L7.365 5.847l-5.051.734A1.535 1.535 0 0 0 1.463 9.2l3.656 3.563-.863 5.031a1.532 1.532 0 0 0 2.226 1.616L11 17.033l4.518 2.375a1.534 1.534 0 0 0 2.226-1.617l-.863-5.03L20.537 9.2a1.523 1.523 0 0 0 .387-1.575Z"/>
            </svg>
            <svg class="w-4 h-4 ms-1" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="currentColor" viewBox="0 0 22 20" :style="[wanteds > 3 ? { 'color': '#fde047' } : { 'color': '#d1d5db', 'opacity': '50%'} ]">
              <path d="M20.924 7.625a1.523 1.523 0 0 0-1.238-1.044l-5.051-.734-2.259-4.577a1.534 1.534 0 0 0-2.752 0L7.365 5.847l-5.051.734A1.535 1.535 0 0 0 1.463 9.2l3.656 3.563-.863 5.031a1.532 1.532 0 0 0 2.226 1.616L11 17.033l4.518 2.375a1.534 1.534 0 0 0 2.226-1.617l-.863-5.03L20.537 9.2a1.523 1.523 0 0 0 .387-1.575Z"/>
            </svg>
            <svg class="w-4 h-4 ms-1" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="currentColor" viewBox="0 0 22 20" :style="[wanteds > 4 ? { 'color': '#fde047' } : { 'color': '#d1d5db', 'opacity': '50%'} ]">
              <path d="M20.924 7.625a1.523 1.523 0 0 0-1.238-1.044l-5.051-.734-2.259-4.577a1.534 1.534 0 0 0-2.752 0L7.365 5.847l-5.051.734A1.535 1.535 0 0 0 1.463 9.2l3.656 3.563-.863 5.031a1.532 1.532 0 0 0 2.226 1.616L11 17.033l4.518 2.375a1.534 1.534 0 0 0 2.226-1.617l-.863-5.03L20.537 9.2a1.523 1.523 0 0 0 .387-1.575Z"/>
            </svg>
          </div>
          <h1 class="text-2xl font-bold">$ {{ getMoneyFormat }}</h1>
          <h1 class="leading-4 text-lg font-bold">$ {{ getBankFormat }}</h1>
        </div>
      </div>

      <div class="text-end">
        <div>
          <p class="font-bold">{{ weapon.name }}</p>
          <div class="flex justify-end">
            <p class="text-green-400 font-bold">{{ weapon.ammo }}</p><p class="text-gray-300 text-xs opacity-75"><span class="mx-1">/</span>{{ weapon.clipSize }}</p>
          </div>
        </div>
      </div>

      <div class="text-end">
        <div class="flex justify-end">
          <div class="border rounded-xl p-1" :style="speech ? { 'background-color': 'rgba(255,255,255,.75)' } : { 'background-color': 'unset' }">
            <svg class="w-7" viewBox="0 0 32 32" version="1.1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink">
              <g id="icomoon-ignore"></g>
              <path d="M16.003 22.377c3.231 0 5.851-2.619 5.851-5.851v-10.639c0-3.231-2.62-5.85-5.851-5.85s-5.851 2.619-5.851 5.85v10.639c0 3.231 2.62 5.851 5.851 5.851zM11.216 5.888c0-2.639 2.147-4.786 4.787-4.786s4.787 2.147 4.787 4.786v10.639c0 2.64-2.147 4.787-4.787 4.787s-4.787-2.147-4.787-4.787v-10.639z" :style="speech ? { 'fill': '#000' } : { 'fill': '#fff' }"></path>
              <path d="M23.978 11.207v5.319c0 4.399-3.579 7.978-7.978 7.978s-7.978-3.579-7.978-7.978v-5.319h-1.064v5.319c0 4.83 3.81 8.776 8.581 9.018h-0.068v5.354h-4.79v1.064h10.637v-1.064h-4.784v-5.354h-0.073c4.771-0.243 8.581-4.189 8.581-9.018v-5.319h-1.064z" :style="speech ? { 'fill': '#000' } : { 'fill': '#fff' }"></path>
            </svg>
          </div>
        </div>
      </div>
    </div>

    <!-- TACHO -->
    <div v-if="vehicle.entered === true" class="absolute bottom-4 right-4 text-end">
      <div class="grid gap-y-1">
        <h1 class="leading-4 text-xl font-semibold">{{ vehicle.speed }} KM/H</h1>
          <div class="h-1 w-full bg-neutral-200 bg-opacity-90">
            <div class="h-1 bg-yellow-400" :style="{'width': `${vehicle.fuel}%`}"></div>
          </div>
          <div class="w-[8vw] bg-black bg-opacity-70 rounded-md flex gap-x-2 justify-center py-1">
            <svg class="w-6" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
              <path d="M12 16H12.01M12 11V13M14 8V5M11 5H17M6 12H3M3 9V15M21 11V19M6 8V16H8L10 19H18V10L16 8H6Z" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" :style="vehicle.engineOn ? { 'stroke': '#3ac33d' } : { 'stroke': '#fff' }"/>
            </svg>
            <svg class="w-6" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
              <path d="M12 14.5V16.5M7 10.0288C7.47142 10 8.05259 10 8.8 10H15.2C15.9474 10 16.5286 10 17 10.0288M7 10.0288C6.41168 10.0647 5.99429 10.1455 5.63803 10.327C5.07354 10.6146 4.6146 11.0735 4.32698 11.638C4 12.2798 4 13.1198 4 14.8V16.2C4 17.8802 4 18.7202 4.32698 19.362C4.6146 19.9265 5.07354 20.3854 5.63803 20.673C6.27976 21 7.11984 21 8.8 21H15.2C16.8802 21 17.7202 21 18.362 20.673C18.9265 20.3854 19.3854 19.9265 19.673 19.362C20 18.7202 20 17.8802 20 16.2V14.8C20 13.1198 20 12.2798 19.673 11.638C19.3854 11.0735 18.9265 10.6146 18.362 10.327C18.0057 10.1455 17.5883 10.0647 17 10.0288M7 10.0288V8C7 5.23858 9.23858 3 12 3C14.7614 3 17 5.23858 17 8V10.0288" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" :style="vehicle.lockState === 2 ? { 'stroke': '#e1d815' } : { 'stroke': '#fff' }"/>
            </svg>
          </div>
      </div>
    </div>
  </main>
</template>

<script>
if (window.alt === undefined) {
  window.alt = {
    emit: () => {},
    on: () => {},
    log: () => {},
    everyTick: () => {},
    clearEveryTick: () => {},
  };
}

export default {
  data: () => ({
    currentDate: null,
    currentTime: null,
    userId: null,
    userStaticId: null,
    money: 0,
    bank: 0,
    wanteds: 0,
    vehicle: {
      entered: false,
      speed: 0,
      engineOn: false,
      lockState: 0,
      fuel: 0,
    },
    weapon: {
      name: "?",
      ammo: 0,
      clipSize: 0,
    },
    speech: false,
  }),
  mounted() {
    const today = new Date();
    this.currentDate = today.toLocaleDateString('de-DE');

    setInterval(() => {
      const current = new Date();
      this.currentTime = current.getHours() + ":" + current.getMinutes();
    }, 150);

    alt.on("hud:create", (id, sid) => {
      this.userId = id;
      this.userStaticId = sid;
    });
    alt.on('hud:update', (info, weapon, speech) => {
      if (info !== null) {
        this.wanteds = info.wanteds;
        this.money = info.money,
        this.bank = info.bank;
      }
      this.weapon = weapon;
      this.speech = speech;
    });

    alt.on('hud:vehicle:enter', () => this.vehicle.entered = true);
    alt.on('hud:vehicle:leave', () => this.vehicle.entered = false);
    alt.on('hud:vehicle:update', (speed, engineOn, lockState, fuel) => {
      this.vehicle.speed = Math.round(speed * 3.6);
      this.vehicle.engineOn = engineOn;
      this.vehicle.lockState = lockState;
      this.vehicle.fuel = fuel;
    });
  },
  computed: {
    getMoneyFormat() {
      return this.numberWithSpaces(this.money);
    },
    getBankFormat() {
      return this.numberWithSpaces(this.bank);
    }
  },
  methods: {
    numberWithSpaces(x) {
      return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ");
    }
  }
}
</script>
