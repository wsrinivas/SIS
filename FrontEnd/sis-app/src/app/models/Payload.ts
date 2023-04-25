import { Retrospective } from "./Retrospective";

export interface Payload{
    Retrospectives : Retrospective[];
    count: number;
}