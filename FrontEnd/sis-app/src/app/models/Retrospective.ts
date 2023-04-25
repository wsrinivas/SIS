import { Feedback } from "./Feedback";

export interface Retrospective{
    id?: string | null,
    name?: string | null,
    summary?: string | null,
    date?: string | null,
    participants?: string[],
    feedbacks : Feedback[] | null,

}