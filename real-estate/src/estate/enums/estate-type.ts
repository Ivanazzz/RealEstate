export enum EstateType {
  OneRoom = 1,
  TwoRooms = 2,
  ThreeRooms = 3,
  FourRooms = 4,
  Office = 5,
  House = 6,
  Shop = 7,
}

export const EstateTypeLocalization = {
  [EstateType.OneRoom]: "1-стаен",
  [EstateType.TwoRooms]: "2-стаен",
  [EstateType.ThreeRooms]: "3-стаен",
  [EstateType.FourRooms]: "4-стаен",
  [EstateType.Office]: "офис",
  [EstateType.House]: "къща",
  [EstateType.Shop]: "магазин",
};
