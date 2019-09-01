module.exports = {
  env: {
    browser: true,
    commonjs: true,
    es6: true,
  },
  extends: 'eslint:recommended',
  globals: {
    process: 'writable',
    tinymce: 'writable',
  },
  parserOptions: {
    ecmaFeatures: {
      jsx: true,
    },
    ecmaVersion: 2018,
    sourceType: 'module',
  },
  plugins: ['react'],
  rules: {
    indent: ['error', 2, { SwitchCase: 1 }],
    'comma-dangle': ['error', 'always-multiline'],
    'linebreak-style': ['error', 'unix'],
    quotes: ['error', 'single'],
    semi: ['error', 'always'],
    'react/jsx-uses-react': 'error',
    'react/jsx-uses-vars': 'error',
    'no-console': 'warn',
    'no-useless-escape': 'off',
  },
};
