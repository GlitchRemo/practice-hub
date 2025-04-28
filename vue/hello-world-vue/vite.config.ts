import { fileURLToPath, URL } from 'node:url'

import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import vueJsx from '@vitejs/plugin-vue-jsx'
import vueDevTools from 'vite-plugin-vue-devtools'

// https://vite.dev/config/
export default defineConfig({
  plugins: [
    vue(),
    vueJsx(),
    vueDevTools(),
  ],
  resolve: {
    alias: {
      '@': fileURLToPath(new URL('./src', import.meta.url))
    },
  },
  test: {
    globals: true, // Use globals like `describe` and `it` without imports
    environment: 'jsdom', // Use jsdom for testing in a browser-like environment
    transformMode: {
      web: [/\.vue$/],
    },
    // Enable coverage reporting
    coverage: {
      reporter: ['text', 'json', 'html'],
      exclude: ['**/main.ts', '**config.ts', 'env.d.ts'],
    },
  },
})
