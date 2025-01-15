module.exports = {
  content: [
    "./src/**/*.{js,jsx,ts,tsx}",
  ],
  safelist: [
    {
      pattern: /bg-primary/, // Safelist classes like bg-primary, bg-primary-light
    },
  ],
  theme: {
    extend: {
      colors: {
        primary: {
          DEFAULT: "#3b82f6",
          dark: "#1e40af",
        },
      },
    },
  },
  plugins: [],
};
