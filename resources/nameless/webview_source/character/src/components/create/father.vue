<template>
  <div class="bg-black bg-opacity-75 rounded-sm py-2 px-3">
    <div>
      <p class="text-base">Vater <span class="text-sm text-gray-300">{{ fathersName[customization.father] }}</span></p>
    </div>
    <input v-model="customization.father" @change="this.$emit('updatePed')" type="range" min="0" :max="fathers.length - 1" class="w-full h-2 bg-gray-200 rounded-lg appearance-none cursor-pointer dark:bg-gray-700">
  </div>

  <div class="bg-black bg-opacity-75 rounded-sm py-2 px-3">
    <div>
      <p class="text-base">Vater(Haut) <span class="text-sm text-gray-300">{{ fathersName[customization.fatherSkin] }}</span></p>
    </div>
    <input v-model="customization.fatherSkin" @change="this.$emit('updatePed')" type="range" min="0" :max="fathers.length - 1" class="w-full h-2 bg-gray-200 rounded-lg appearance-none cursor-pointer dark:bg-gray-700">
  </div>
</template>

<script>
export default {
  props: ['customization'],
  emits: {
    updatePed: null,
  },
  data: () => ({
    fathers: [
      0,
      1,
      2,
      3,
      4,
      5,
      6,
      7,
      8,
      9,
      10,
      11,
      12,
      13,
      14,
      15,
      16,
      17,
      18,
      19,
      20,
      42,
      43,
      44,
    ],
    fathersName: [
      'Benjamin',
      'Daniel',
      'Joshua',
      'Noah',
      'Andrew',
      'Joan',
      'Alex',
      'Isaac',
      'Evan',
      'Ethan',
      'Vincent',
      'Angel',
      'Diego',
      'Adrian',
      'Gabriel',
      'Michael',
      'Santiago',
      'Kevin',
      'Louis',
      'Samuel',
      'Anthony',
      'John',
      'Niko',
      'Claude',
    ],
  }),
}
</script>