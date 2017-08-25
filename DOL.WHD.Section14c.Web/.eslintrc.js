module.exports = {
    "env": {
        "browser": true,
        "commonjs": true,
        "es6": true
    },
    "globals": {
        "angular": 1
    },
    "extends": "eslint:recommended",
    "parserOptions": {
        "ecmaFeatures": {
            "jsx": true
        },
        "sourceType": "module"
    },
    "rules": {

        // Possible Errors
        "comma-dangle": 2,
        "no-cond-assign": 2,
        "no-console": 0,
        "no-constant-condition": 2,
        "no-control-regex": 2,
        "no-debugger": 2,
        "no-dupe-args": 2,
        "no-dupe-keys": 2,
        "no-duplicate-case": 2,
        "no-empty": 2,
        "no-empty-character-class": 2,
        "no-ex-assign": 2,
        "no-extra-boolean-cast": 2,
        "no-extra-parens": 0,
        "no-extra-semi": 2,
        "no-func-assign": 2,
        "no-inner-declarations": [2, "functions"],
        "no-invalid-regexp": 2,
        "no-irregular-whitespace": 2,
        "no-negated-in-lhs": 2,
        "no-obj-calls": 2,
        "no-regex-spaces": 2,
        "no-sparse-arrays": 2,
        "no-unexpected-multiline": 2,
        "no-unreachable": 2,
        "use-isnan": 2,
        "valid-jsdoc": 0,
        "valid-typeof": 2,

        //Best Practices
        "accessor-pairs": 2,
        "block-scoped-var": 0,
        "complexity": [2, 6],
        "consistent-return": 0,
        "curly": 0,
        "default-case": 0,
        "dot-location": 0,
        "dot-notation": 0,
        "eqeqeq": 2,
        "guard-for-in": 2,
        "no-alert": 2,
        "no-caller": 2,
        "no-case-declarations": 2,
        "no-div-regex": 2,
        "no-else-return": 0,
        "no-empty-label": 0,
        "no-empty-pattern": 2,
        "no-eq-null": 2,
        "no-eval": 2,
        "no-extend-native": 2,
        "no-extra-bind": 2,
        "no-fallthrough": 2,
        "no-floating-decimal": 0,
        "no-implicit-coercion": 0,
        "no-implied-eval": 2,
        "no-invalid-this": 0,
        "no-iterator": 2,
        "no-labels": 0,
        "no-lone-blocks": 2,
        "no-loop-func": 2,
        "no-magic-number": 0,
        "no-multi-spaces": 0,
        "no-multi-str": 0,
        "no-native-reassign": 0,
        "no-new-func": 2,
        "no-new-wrappers": 2,
        "no-new": 2,
        "no-octal-escape": 2,
        "no-octal": 2,
        "no-proto": 2,
        "no-redeclare": 2,
        "no-return-assign": 2,
        "no-script-url": 2,
        "no-self-compare": 2,
        "no-sequences": 0,
        "no-throw-literal": 0,
        "no-unused-expressions": 2,
        "no-useless-call": 2,
        "no-useless-concat": 2,
        "no-void": 2,
        "no-warning-comments": 0,
        "no-with": 2,
        "radix": 2,
        "vars-on-top": 0,
        "wrap-iife": 2,
        "yoda": 0,

        // Strict
        "strict": 0,

        // Variables
        "init-declarations": 0,
        "no-catch-shadow": 2,
        "no-delete-var": 2,
        "no-label-var": 2,
        "no-shadow-restricted-names": 2,
        "no-shadow": 0,
        "no-undef-init": 2,
        "no-undef": 0,
        "no-undefined": 0,
        "no-unused-vars": 0,
        "no-use-before-define": 0,

        // Node.js and CommonJS
        "callback-return": 0,
        "global-require": 0,
        "handle-callback-err": 0,
        "no-mixed-requires": 0,
        "no-new-require": 0,
        "no-path-concat": 0,
        "no-process-exit": 0,
        "no-restricted-modules": 0,
        "no-sync": 0,

        //Stylistic Issues
        "array-bracket-spacing": 0,
        "block-spacing": 0,
        "brace-style": 0,
        "camelcase": 0,
        "comma-spacing": 0,
        "comma-style": 0,
        "computed-property-spacing": 0,
        "consistent-this": 0,
        "eol-last": 0,
        "func-names": 0,
        "func-style": 0,
        "id-length": 0,
        "id-match": 0,
        "indent": 0,
        "jsx-quotes": 0,
        "key-spacing": 0,
        "linebreak-style": 0,
        "lines-around-comment": 0,
        "max-depth": 0,
        "max-len": 0,
        "max-nested-callbacks": 0,
        "max-params": 0,
        "max-statements": [2, 50],
        "new-cap": 0,
        "new-parens": 0,
        "newline-after-var": 0,
        "no-array-constructor": 0,
        "no-bitwise": 0,
        "no-continue": 0,
        "no-inline-comments": 0,
        "no-lonely-if": 0,
        "no-mixed-spaces-and-tabs": 0,
        "no-multiple-empty-lines": 0,
        "no-negated-condition": 0,
        "no-nested-ternary": 0,
        "no-new-object": 0,
        "no-plusplus": 0,
        "no-restricted-syntax": 0,
        "no-spaced-func": 0,
        "no-ternary": 0,
        "no-trailing-spaces": 0,
        "no-underscore-dangle": 0,
        "no-unneeded-ternary": 0,
        "object-curly-spacing": 0,
        "one-var": 0,
        "operator-assignment": 0,
        "operator-linebreak": 0,
        "padded-blocks": 0,
        "quote-props": 0,
        "quotes": 0,
        "require-jsdoc": 0,
        "semi-spacing": 0,
        "semi": 0,
        "sort-vars": 0,
        "space-after-keywords": 0,
        "space-before-blocks": 0,
        "space-before-function-paren": 0,
        "space-before-keywords": 0,
        "space-in-parens": 0,
        "space-infix-ops": 0,
        "space-return-throw-case": 0,
        "space-unary-ops": 0,
        "spaced-comment": 0,
        "wrap-regex": 0,

        // ECMAScript 6
        "arrow-body-style": 0,
        "arrow-parens": 0,
        "arrow-spacing": 0,
        "constructor-super": 0,
        "generator-star-spacing": 0,
        "no-arrow-condition": 0,
        "no-class-assign": 0,
        "no-const-assign": 0,
        "no-dupe-class-members": 0,
        "no-this-before-super": 0,
        "no-var": 0,
        "object-shorthand": 0,
        "prefer-arrow-callback": 0,
        "prefer-const": 0,
        "prefer-reflect": 0,
        "prefer-spread": 0,
        "prefer-template": 0,
        "require-yield": 0
    }
};
