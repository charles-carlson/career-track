const { build } = require('esbuild');

build({
    entryPoints: ['wwwroot/Scripts/Home/Home.js'],
    bundle: true,
    outfile: 'wwwroot/dist/bundle.js',
    minify: true,
}).catch(() => process.exit(1));

