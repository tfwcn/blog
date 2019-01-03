module.exports = {
  parser: 'babel-eslint',
  env: {
    browser: true,
    commonjs: true,
    es6: true
  },
  extends: ['airbnb', 'eslint:recommended', 'plugin:react/recommended'],
  globals: {
    process: true
  },
  parserOptions: {
    ecmaFeatures: {
      jsx: true
    },
    ecmaVersion: 2018,
    sourceType: 'module'
  },
  plugins: ['react'],
  rules: {
    "react/jsx-filename-extension": ['error', { "extensions": [".js", ".jsx"] }],
    indent: ['error', 2],
    'linebreak-style': ['error', 'unix'],
    quotes: ['error', 'single'],
    semi: ['error', 'always'],
    'react/jsx-uses-react': 'error',
    'react/jsx-uses-vars': 'error',
    'no-console': 'warn'
  }
};
