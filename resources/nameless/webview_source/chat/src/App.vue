<template>
  <main v-if="hidden === false" class="pl-1 pt-2" :style="opened === true ? { 'opacity': '100%' } : { 'opacity': '75%' }">
    <div id="messageContainer" class="h-[14vw] w-[31.45vw] text-white overflow-y-auto">
      <div class="grid gap-y-1">
        <div v-for="(msg, index) in messages" :key="index">
          <div class="flex items-start gap-x-2 rounded-md py-1 px-2" :style="{ 'background-color': messageColors[msg.type] }">
            <div v-if="msg.from !== null" class="whitespace-nowrap">
              <p class="text-base">{{ msg.from }}:</p>
            </div>
            <p class="text-base">{{ msg.text }}</p>
          </div>
        </div>
      </div>
    </div>
    <div class="w-[31.45vw] pt-2" :style="opened === true ? { 'display': 'block' } : { 'display': 'none' }">
      <input v-model="newMessage" v-on:keyup.enter="sendMessage()" class="w-full bg-black bg-opacity-75 text-lg text-white focus:outline-none py-1 px-2" type="text" id="messageBox" maxlength="255">
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
    hidden: false,
    hiddenTimeout: null,
    opened: false,
    messages: [],
    newMessage: "",
    messageColors: [
      'rgba(0, 0, 0, .75)',
      'rgba(9, 141, 155, .75)',
      'rgba(201, 177, 9, .75)',
      'rgba(177, 8, 8, .75)',
    ],
  }),
  mounted() {
    alt.on('chat:open', () => {
      this.clearHidden();
      
      this.opened = true;
      this.$nextTick(() => {
        const messageBox = this.$el.querySelector('#messageBox');
        messageBox.focus();
        const messageContainer = this.$el.querySelector('#messageContainer');
        messageContainer.scrollTop = messageContainer.scrollHeight;
      });
    });

    alt.on('chat:close', () => {
      this.opened = false;
      this.hiddenTimeout = setTimeout(() => this.hidden = true, 3500);
    });

    alt.on('chat:message', (msg) => {
      this.messages.push(msg);

      this.clearHidden();

      if (this.opened === false) {
        this.hiddenTimeout = setTimeout(() => this.hidden = true, 3500);
      }

      this.$nextTick(() => {
        const messageContainer = this.$el.querySelector('#messageContainer');
        messageContainer.scrollTop = messageContainer.scrollHeight;
      });
    });
  },
  methods: {
    clearHidden() {
      if (this.hidden === true) {
        this.hidden = false;
      }
      if (this.hiddenTimeout !== null) {
        clearTimeout(this.hiddenTimeout);
        this.hiddenTimeout = null;
      }
    },
    sendMessage() {
      if (this.newMessage.length === 0) {
        return;
      }
      
      alt.emit('chat:message', this.newMessage);
      this.newMessage = "";
    }
  }
}
</script>

<style>
::-webkit-scrollbar {
  display: none;
}
</style>