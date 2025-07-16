/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./**/*.razor",
    "./**/*.html",
    "./wwwroot/lib/**/*.js"
  ],
  darkMode: 'class',
  theme: {
    extend: {},
  },
  plugins: [
    require('flowbite/plugin')
  ],
}
