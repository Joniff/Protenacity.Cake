import type { PosmCoordinate } from "./posm-coordinate";     
import type { PosmBoundingCoordinates } from "./posm-bounding-coordinates";     

export type PosmMap = {
    zoom: number,
    marker: PosmCoordinate | null,
    boundingBox: PosmBoundingCoordinates
};
