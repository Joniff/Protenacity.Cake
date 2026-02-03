import { defineConfig } from 'vite';

export default defineConfig({
    build: {
        lib: {
            entry: ['icons/icons.ts', 'src/entrypoints.ts'],
            formats: ['es']
        },
        outDir: '../wwwroot/App_Plugins/Protenacity.TipTapHeaders',
        emptyOutDir: true,
        sourcemap: true,
        rollupOptions: {
            external: [/^@umbraco/]
        }
    }
});