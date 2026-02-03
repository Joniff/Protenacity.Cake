import { defineConfig } from 'vite';

export default defineConfig({
    build: {
        lib: {
            entry: 'src/index.ts',
            formats: ['es'],
            fileName: 'protenacity-ufm-percent',
        },
        outDir: '../wwwroot/App_Plugins/Protenacity.UfmPercent', 
        emptyOutDir: true,
        sourcemap: true,
        rollupOptions: {
            external: [/^@umbraco/]
        }
    }
});
