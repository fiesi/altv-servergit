<template>
  <main class="text-white">
    <div v-if="error !== null" class="absolute top-[2.917vw] left-2/4 translate-x-[-50%]">
      <div class="max-w-[20vw] bg-red-600 rounded-md px-2 py-1">
        <p class="text-sm">{{ error }}</p>
      </div>
    </div>

    <router-view @showError="(msg) => showError(msg)" />
  </main>
</template>

<script>
if (window.alt === undefined) {
  window.alt = {
    emit: () => {},
    emitServer: () => {},
    on: () => {},
  };
}

export default {
  data: () => ({
    error: null,
    errorTimeout: null,
  }),
  mounted() {
    alt.on('character:route', (route) => this.$router.push(route));
  },
  methods: {
    showError(msg) {
      if (this.errorTimeout !== null) {
        clearTimeout(this.errorTimeout);
      }

      this.error = msg;
      this.errorTimeout = setTimeout(() => {
        this.error = null;
        this.errorTimeout = null;
      }, 3500);
    },
  }
}
</script>

<style>
::-webkit-scrollbar {
  display: none;
}
</style>