<template>
  <div class="bg-black bg-opacity-75 rounded-sm py-2 px-3">
    <div>
      <p class="text-base">Haare</p>
    </div>
    <div class="h-[7vw] overflow-y-auto ">
      <div v-if="customization.sex === 0" class="grid grid-cols-3 gap-2 p-1">
        <img @click="clothing.hair = availableHairs.male[0]" class="cursor-pointer" src="@/assets/images/create/male/hair_0.jpg" alt="" :style="clothing.hair === availableHairs.male[0] ? { 'box-shadow': '0 0 4px 2px' } : null">
        <img @click="clothing.hair = availableHairs.male[1]" class="cursor-pointer" src="@/assets/images/create/male/hair_1.jpg" alt="" :style="clothing.hair === availableHairs.male[1] ? { 'box-shadow': '0 0 4px 2px' } : null">
        <img @click="clothing.hair = availableHairs.male[2]" class="cursor-pointer" src="@/assets/images/create/male/hair_2.jpg" alt="" :style="clothing.hair === availableHairs.male[2] ? { 'box-shadow': '0 0 4px 2px' } : null">
        <img @click="clothing.hair = availableHairs.male[3]" class="cursor-pointer" src="@/assets/images/create/male/hair_13.jpg" alt="" :style="clothing.hair === availableHairs.male[3] ? { 'box-shadow': '0 0 4px 2px' } : null">
        <img @click="clothing.hair = availableHairs.male[4]" class="cursor-pointer" src="@/assets/images/create/male/hair_75.jpg" alt="" :style="clothing.hair === availableHairs.male[4] ? { 'box-shadow': '0 0 4px 2px' } : null">
        <img @click="clothing.hair = availableHairs.male[5]" class="cursor-pointer" src="@/assets/images/create/male/hair_76.jpg" alt="" :style="clothing.hair === availableHairs.male[5] ? { 'box-shadow': '0 0 4px 2px' } : null">
      </div>
      <div v-else class="grid grid-cols-3 gap-2 p-1">
        <img @click="clothing.hair = availableHairs.female[0]" class="cursor-pointer" src="@/assets/images/create/female/hair_0.jpg" alt="" :style="clothing.hair === availableHairs.female[0] ? { 'box-shadow': '0 0 4px 2px' } : null">
        <img @click="clothing.hair = availableHairs.female[1]" class="cursor-pointer" src="@/assets/images/create/female/hair_7.jpg" alt="" :style="clothing.hair === availableHairs.female[1] ? { 'box-shadow': '0 0 4px 2px' } : null">
        <img @click="clothing.hair = availableHairs.female[2]" class="cursor-pointer" src="@/assets/images/create/female/hair_10.jpg" alt="" :style="clothing.hair === availableHairs.female[2] ? { 'box-shadow': '0 0 4px 2px' } : null">
        <img @click="clothing.hair = availableHairs.female[3]" class="cursor-pointer" src="@/assets/images/create/female/hair_11.jpg" alt="" :style="clothing.hair === availableHairs.female[3] ? { 'box-shadow': '0 0 4px 2px' } : null">
        <img @click="clothing.hair = availableHairs.female[4]" class="cursor-pointer" src="@/assets/images/create/female/hair_65.jpg" alt="" :style="clothing.hair === availableHairs.female[4] ? { 'box-shadow': '0 0 4px 2px' } : null">
        <img @click="clothing.hair = availableHairs.female[5]" class="cursor-pointer" src="@/assets/images/create/female/hair_79.jpg" alt="" :style="clothing.hair === availableHairs.female[5] ? { 'box-shadow': '0 0 4px 2px' } : null">
      </div>
    </div>
  </div>
  
  <div class="bg-black bg-opacity-75 rounded-sm py-2 px-3">
    <div>
      <p class="text-base">Haarfarbe(Primär)</p>
    </div>
    <input v-model="clothing.hairColorPrimary" type="range" min="0" max="63" class="w-full h-2 bg-gray-200 rounded-lg appearance-none cursor-pointer dark:bg-gray-700">
  </div>
  
  <div class="bg-black bg-opacity-75 rounded-sm py-2 px-3">
    <div>
      <p class="text-base">Haarfarbe(Secondär)</p>
    </div>
    <input v-model="clothing.hairColorSecondary" type="range" min="0" max="63" class="w-full h-2 bg-gray-200 rounded-lg appearance-none cursor-pointer dark:bg-gray-700">
  </div>
</template>

<script>
export default {
  props: ['customization', 'clothing'],
  data: () => ({
    availableHairs: {
      male: [
        0,
        1,
        2,
        13,
        75,
        76,
      ],
      female: [
        0,
        7,
        10,
        11,
        65,
        79,
      ],
    },
  })
}
</script>