<template>
  <div class="w-[20vw] pt-2 pl-2">
    <div class="grid gap-y-1">
      <div class="bg-black bg-opacity-75 rounded-sm py-2 px-3">
        <div class="grid grid-cols-2">
          <div>
            <p class="text-base">Vorname</p>
          </div>
          <div>
            <input v-model="firstname" class="text-base bg-transparent focus:outline-none" type="text" placeholder="Max" maxlength="11">
          </div>
        </div>
      </div>
  
      <div class="bg-black bg-opacity-75 rounded-sm py-2 px-3">
        <div class="grid grid-cols-2">
          <div>
            <p class="text-base">Nachname</p>
          </div>
          <div>
            <input v-model="lastname" class="text-base bg-transparent focus:outline-none" type="text" placeholder="Mustermann" maxlength="11">
          </div>
        </div>
      </div>
  
      <div class="bg-black bg-opacity-75 rounded-sm py-2 px-3">
        <div>
          <p class="text-base">Geschlecht</p>
        </div>
        <div class="grid grid-cols-2 gap-x-1">
          <button @click="createPed()" class="w-full text-base bg-[#008736] disabled:bg-opacity-75 rounded-sm py-1 px-2" :disabled="customization.sex === 0">Männlich</button>
          <button @click="createPed()" class="w-full text-base bg-[#008736] disabled:bg-opacity-75 rounded-sm py-1 px-2" :disabled="customization.sex === 1">Weiblich</button>
        </div>
      </div>
      
      <FatherComponent :customization="customization" @updatePed="updatePed()" />
      
      <MotherComponent :customization="customization" @updatePed="updatePed()" />
  
      <div class="bg-black bg-opacity-75 rounded-sm py-2 px-3">
        <div>
          <p class="text-base">Ähnlichkeit <span class="text-sm text-gray-300">{{ customization.faceMix }}</span></p>
        </div>
        <input v-model="customization.faceMix" @change="updatePed()" type="range" max="1" step="0.1" class="w-full h-2 bg-gray-200 rounded-lg appearance-none cursor-pointer dark:bg-gray-700">
      </div>
  
      <div class="bg-black bg-opacity-75 rounded-sm py-2 px-3">
        <div>
          <p class="text-base">Ähnlichkeit(Haut) <span class="text-sm text-gray-300">{{ customization.skinMix }}</span></p>
        </div>
        <input v-model="customization.skinMix" @change="updatePed()" type="range" max="1" step="0.1" class="w-full h-2 bg-gray-200 rounded-lg appearance-none cursor-pointer dark:bg-gray-700">
      </div>
  
      <div class="grid gap-y-1 p-1">
        <button @click="save()" class="w-full text-base bg-[#008736] hover:shadow-[0_0_7px_1px_#008736] rounded-sm py-1 px-2 transition">Speichern</button>
        <button @click="cancel()" class="w-full text-base bg-[#cf0909] hover:shadow-[0_0_7px_1px_#cf0909] rounded-sm py-1 px-2 transition">Abbrechen</button>
      </div>
    </div>
  </div>
  
  <!-- RIGHT SIDE -->
  <div class="w-[20vw] absolute grid gap-y-1 top-2 right-2">
    <HairComponent :customization="customization" :clothing="clothing" />
  </div>
</template>

<script>
import FatherComponent from '@/components/create/father.vue';
import MotherComponent from '@/components/create/mother.vue';
import HairComponent from '@/components/create/hair.vue';

export default {
  components: {
    FatherComponent,
    MotherComponent,
    HairComponent,
  },
  emits: {
    showError: null,
  },
  data: () => ({
    firstname: "",
    lastname: "",
    customization: {
      sex:        0,
      father:     0,
      fatherSkin: 0,
      mother:     0,
      motherSkin: 0,
      faceMix:    0,
      skinMix:    0,
    },
    clothing: {
      hair:               0,
      hairColorPrimary:   0,
      hairColorSecondary: 0,
    }
  }),
  mounted() {
    alt.on('character:creation:error', (msg) => this.$emit('showError', msg));
  },
  watch: {
    clothing: {
      handler(newValue, _) {
        alt.emit('character:creation:update:ped', null, newValue);
      },
      deep: true,
    }
  },
  methods: {
    updatePed() {
      alt.emit('character:creation:update:ped', this.customization, null);
    },
    createPed() {
      this.customization = {
        sex:        this.customization.sex === 1 ? 0 : 1,
        father:     0,
        fatherSkin: 0,
        mother:     0,
        motherSkin: 0,
        faceMix:    0,
        skinMix:    0,
      };
      this.clothing = {
        hair:               0,
        hairColorPrimary:   0,
        hairColorSecondary: 0,
      }
      alt.emit('character:creation:create:ped', this.customization, null);
    },
    save() {
      if (this.firstname.length < 3) {
        this.$emit('showError', 'Dieser Vorname ist zu kurz!');
        return;
      } else if (this.firstname.length > 11) {
        this.$emit('showError', 'Dieser Vorname ist zu lang!');
        return;
      }

      if (this.lastname.length < 3) {
        this.$emit('showError', 'Dieser Nachname ist zu kurz!');
        return;
      } else if (this.lastname.length > 11) {
        this.$emit('showError', 'Dieser Nachname ist zu lang!');
        return;
      }

      alt.emit('character:creation:save', this.firstname, this.lastname, this.customization, this.clothing);
    },
    cancel() {
      alt.emit('character:creation:cancel');
    }
  }
}
</script>