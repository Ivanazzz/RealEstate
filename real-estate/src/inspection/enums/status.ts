export enum Status {
  Stated = 1,
  Confirmed = 2,
  Declined = 3,
}

export const StatusLocalization = {
  [Status.Stated]: "Заявен",
  [Status.Confirmed]: "Потвърден",
  [Status.Declined]: "Отказан",
};
