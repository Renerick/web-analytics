const purgecss = require("@fullhuman/postcss-purgecss")({
    content: [
        './src/**/*.html',
        './src/**/*.js',
        './src/**/*.svelte',
        './public/index.html'
    ],
    defaultExtractor: content => content.match(/[\w-/:]+(?<!:)/g) || [],
    whitelistPatterns: [/^svelte-/],
});


module.exports = (ctx) => ({
    plugins: [
        require("tailwindcss"),
        require("autoprefixer"),
        ...ctx.options.env === "production"
            ? [
                purgecss,
                require("cssnano")({
                    preset: "default"
                })
            ]
            : []

    ]
})