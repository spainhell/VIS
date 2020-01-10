export class Vehicle {
  id: number;
  typeId: number;
  brandId: number;
  title: string;
  vin: string;
  licensePlate: string;
  vintage: number;
  price: number;
  purchasedOn: Date;
  driverId: number;
  adminId: number;
}

export class VehicleType {
  id: number;
  typeName: string;
}

export class VehicleBrand {
  id: number;
  brandName: string;
  logoUrl: string;
}
