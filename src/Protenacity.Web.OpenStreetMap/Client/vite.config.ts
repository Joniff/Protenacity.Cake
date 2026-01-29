import { defineConfig } from 'vite';

export default defineConfig({
    build: {
        lib: {
            entry: 'src/posm-openstreetmap-property-editor.ts', // your web component source file
            formats: ['es'],
            fileName: 'protenacity-openstreetmap',

        },
        outDir: '../wwwroot/App_Plugins/Protenacity.OpenStreetMap', // your web component will be saved in this location
        emptyOutDir: true,
        sourcemap: true,
        rollupOptions: {
            external: [/^@umbraco/],
        },
    }
});
