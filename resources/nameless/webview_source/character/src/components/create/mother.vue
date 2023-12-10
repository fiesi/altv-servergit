<template>
  <div class="bg-black bg-opacity-75 rounded-sm py-2 px-3">
    <div>
      <p class="text-base">Mutter <span class="text-sm text-gray-300">{{ mothersName[customization.mother] }}</span></p>
    </div>
    <input v-model="customization.mother" @change="this.$emit('updatePed')" type="range" min="0" :max="mothers.length - 1" class="w-full h-2 bg-gray-200 rounded-lg appearance-none cursor-pointer dark:bg-gray-700">
  </div>
  
  <div class="bg-black bg-opacity-75 rounded-sm py-2 px-3">
    <div>
      <p class="text-base">Mutter(Haut) <span class="text-sm text-gray-300">{{ mothersName[customization.motherSkin] }}</span></p>
    </div>
    <input v-model="customization.motherSkin" @change="this.$emit('updatePed')" type="range" min="0" :max="mothers.length - 1" class="w-full h-2 bg-gray-200 rounded-lg appearance-none cursor-pointer dark:bg-gray-700">
  </div>
</template>

<script>
export default {
  props: ['customization'],
  emits: {
    updatePed: null,
  },
  data: () => ({
    mothers: [
      21,
      22,
      23,
      24,
      25,
      26,
      27,
      28,
      29,
      30,
      31,
      32,
      33,
      34,
      35,
      36,
      37,
      38,
      39,
      40,
      41,
      45,
    ],
    mothersName: [
      'Hannah',
      'Audrey',
      'Jasmine',
      'Giselle',
      'Amelia',
      'Isabella',
      'Zoe',
      'Ava',
      'Camilla',
      'Violet',
      'Sophia',
      'Eveline',
      'Nicole',
      'Ashley',
      'Grace',
      'Brianna',
      'Natalie',
      'Olivia',
      'Elizabeth',
      'Charlotte',
      'Emma',
      'Misty',
    ],
  }),
}
</script>