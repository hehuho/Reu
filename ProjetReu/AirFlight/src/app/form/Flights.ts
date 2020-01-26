export class Flights{
  flightId: number;
  flightName: string;
  classeList: {
    classeId: number;
    flightId: number;
    name: string;
    nbSiege: number;
    price: number;
  }


}
