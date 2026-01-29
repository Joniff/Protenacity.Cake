import { defineConfig } from 'vite';

export default defineConfig({
    build: {
        lib: {
            entry: ['src/bundle.manifests.ts'], 
            formats: ['es'],
            fileName: 'review'
        },
        outDir: '../wwwroot/App_Plugins/Protenacity.Review', // your web component will be saved in this location
        emptyOutDir: true,
        sourcemap: true,
        rollupOptions: {
            external: [/^@umbraco/],
        },
    }
});
