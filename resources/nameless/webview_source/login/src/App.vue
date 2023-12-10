<template>
  <main class="w-[33.45vw] text-white bg-[#191919] rounded-xl p-8">
    <div>
      <form class="grid gap-y-2" onsubmit="event.preventDefault()">
        <div class="text-center">
          <h1 class="text-2xl font-semibold">Melde dich an</h1>
          <p class="leading-4 text-sm text-gray-400">Drücke auf "Anmelden mit Discord" um dich mit deinem Discord zu verbinden und dich bei uns anzumelden.</p>
          <p v-if="error !== null" class="text-base text-red-700 font-semibold">{{ error }}</p>
        </div>

        <button class="text-lg font-semibold bg-[#5865f2] hover:enabled:bg-[#4e5adf] rounded-lg py-2 disabled:opacity-50" type="submit" @click="login()" :disabled="buttonState">Anmelden mit Discord</button>
        <div v-if="buttonState === true" class="text-lg flex justify-center items-center">
          <svg class="animate-spin -ml-1 mr-3 h-5 w-5 text-white" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24">
            <circle class="opacity-25" cx="12" cy="12" r="10" stroke="currentColor" stroke-width="4"></circle>
            <path class="opacity-75" fill="currentColor" d="M4 12a8 8 0 018-8V0C5.373 0 0 5.373 0 12h4zm2 5.291A7.962 7.962 0 014 12H0c0 3.042 1.135 5.824 3 7.938l3-2.647z"></path>
          </svg>
          <p>Du wirst angemeldet ...</p>
        </div>
      </form>
    </div>
  </main>
</template>

<script>
if (window.alt === undefined) {
  window.alt = {
    emit: () => {},
    on: () => {}
  };
}

export default {
  data: () => ({
    buttonState: false,
    error: null
  }),
  mounted() {
    alt.on("login:error", (msg) => {
      this.buttonState = false;
      this.error = msg;
    });
  },
  methods: {
    login() {
      this.buttonState = true;
      alt.emit("login:pressed");
    }
  }
}
</script>
