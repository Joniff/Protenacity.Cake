import { defineConfig } from 'vite';

export default defineConfig({
    build: {
        lib: {
            entry: ['icons/protenacity-icons.ts', 'src/protenacity-entrypoints.ts'],
            formats: ['es']
        },
        outDir: '../wwwroot/App_Plugins/protenacity.Forms',
        emptyOutDir: true,
        sourcemap: true,
        rollupOptions: {
            external: [/^@umbraco/]
        }
    }
});