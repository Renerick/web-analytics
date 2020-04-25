import resolve from 'rollup-plugin-node-resolve';
import commonjs from 'rollup-plugin-commonjs';
import {uglify} from 'rollup-plugin-uglify';
import babel from 'rollup-plugin-babel';
import babelrc from 'babelrc-rollup';

import pkg from './package.json';

export default [
    {
        input: 'src/main.js',
        output: {
            name: 'webanalytics',
            file: pkg.browser,
            format: 'umd',
            globals: {
                "crypto": "crypto"
            }
        },
        external: ["crypto"],
        plugins: [
            resolve(),
            commonjs(),
            babel(babelrc()),
            uglify()
        ]
    }
];