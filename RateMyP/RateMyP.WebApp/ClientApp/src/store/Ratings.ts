import { Tag } from './Tags';

export interface Rating {
    id: string,
    teacherId: string;
    courseId: string;
    overallMark: number;
    levelOfDifficulty: number;
    wouldTakeTeacherAgain: boolean;
    dateCreated: Date;
    comment: string;
    tags: Tag[];
    ratingType: number;
    thumbUps?: number;
    thumbDowns?: number;
}

export interface RatingThumb {
    ratingId: string;
    thumbUp: boolean;
}
