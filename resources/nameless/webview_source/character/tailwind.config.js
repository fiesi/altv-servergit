/** @type {import('tailwindcss').Config} */
export default {
  content: [
    "./index.html",
    "./src/**/*.{js,ts,jsx,tsx,vue}",
  ],
  theme: {
    extend: {},
    fontSize: {
      'xs': ['0.625vw', {
        lineHeight: '0.833vw'
      }],
      'sm': ['0.729vw', {
        lineHeight: '1.042vw'
      }],
      'base': ['0.833vw', {
        lineHeight: '1.25vw'
      }],
      'lg': ['0.938vw', {
        lineHeight: '1.458vw'
      }],
      'xl': ['1.042vw', {
        lineHeight: '1.458vw'
      }],
      '2xl': ['1.25vw', {
        lineHeight: '1.667vw'
      }],
    },
    padding: {
      1: '0.208vw',  // 0.25rem
      2: '0.417vw',  // 0.5rem
      3: '0.625vw',  // 0.75rem
      4: '0.833vw',  // 1rem
      5: '1.042vw',  // 1.25rem
      6: '1.25vw',   // 1.5rem
      7: '1.458vw',  // 1.75rem
      8: '1.667vw',  // 2rem
      9: '1.875vw',  // 2.25rem
      10: '2.083vw', // 2.5rem
      11: '2.292vw', // 2.75rem
      12: '2.5vw',   // 3rem
    },
    margin: {
      'auto': 'auto',
      1: '0.208vw',  // 0.25rem
      2: '0.417vw',  // 0.5rem
      3: '0.625vw',  // 0.75rem
      4: '0.833vw',  // 1rem
      5: '1.042vw',  // 1.25rem
      6: '1.25vw',   // 1.5rem
      7: '1.458vw',  // 1.75rem
      8: '1.667vw',  // 2rem
      9: '1.875vw',  // 2.25rem
      10: '2.083vw', // 2.5rem
      11: '2.292vw', // 2.75rem
      12: '2.5vw',   // 3rem
    },
    height: {
      'screen': '100vh',
      'full': '100%',
      1: '0.208vw',  // 0.25rem
      2: '0.417vw',  // 0.5rem
      3: '0.625vw',  // 0.75rem
      4: '0.833vw',  // 1rem
      5: '1.042vw',  // 1.25rem
      6: '1.25vw',   // 1.5rem
      7: '1.458vw',  // 1.75rem
      8: '1.667vw',  // 2rem
      9: '1.875vw',  // 2.25rem
      10: '2.083vw', // 2.5rem
      11: '2.292vw', // 2.75rem
      12: '2.5vw',   // 3rem
    },
    width: {
      'screen': '100vw',
      'full': '100%',
      1: '0.208vw',  // 0.25rem
      2: '0.417vw',  // 0.5rem
      3: '0.625vw',  // 0.75rem
      4: '0.833vw',  // 1rem
      5: '1.042vw',  // 1.25rem
      6: '1.25vw',   // 1.5rem
      7: '1.458vw',  // 1.75rem
      8: '1.667vw',  // 2rem
      9: '1.875vw',  // 2.25rem
      10: '2.083vw', // 2.5rem
      11: '2.292vw', // 2.75rem
      12: '2.5vw',   // 3rem
    },
    gap: {
      1: '0.208vw',  // 0.25rem
      2: '0.417vw',  // 0.5rem
      3: '0.625vw',  // 0.75rem
      4: '0.833vw',  // 1rem
      5: '1.042vw',  // 1.25rem
      6: '1.25vw',   // 1.5rem
      7: '1.458vw',  // 1.75rem
      8: '1.667vw',  // 2rem
      9: '1.875vw',  // 2.25rem
      10: '2.083vw', // 2.5rem
      11: '2.292vw', // 2.75rem
      12: '2.5vw',   // 3rem
    },
    lineHeight: {
      1: '0.208vw',  // 0.25rem
      2: '0.417vw',  // 0.5rem
      3: '0.625vw',  // 0.75rem
      4: '0.833vw',  // 1rem
      5: '1.042vw',  // 1.25rem
      6: '1.25vw',   // 1.5rem
      7: '1.458vw',  // 1.75rem
      8: '1.667vw',  // 2rem
      9: '1.875vw',  // 2.25rem
      10: '2.083vw', // 2.5rem
      11: '2.292vw', // 2.75rem
      12: '2.5vw',   // 3rem
    },
    borderRadius: {
      'sm': '0.104vw',
      'md': '0.313vw',
      'lg': '0.417vw',
      'xl': '0.625vw'
    }
  },
  plugins: [],
}

