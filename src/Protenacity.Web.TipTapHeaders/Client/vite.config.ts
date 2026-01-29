import { defineConfig } from 'vite';

export default defineConfig({
    build: {
        lib: {
            entry: ['icons/lcc-icons.ts', 'src/lcc-entrypoints.ts'],
            formats: ['es']
        },
        outDir: '../wwwroot/App_Plugins/LCCTipTapHeaders',
        emptyOutDir: true,
        sourcemap: true,
        rollupOptions: {
            external: [/^@umbraco/]
        }
    }
});