#include <Wire.h>

#define GYRO_Address 0x68
#define G_SMPLRT_DIV 0x15
#define G_DLPF_FS 0x16
#define G_INT_CFG 0x17
#define G_PWR_MGM 0x3E
#define G_TO_READ 6

int gyro[3];

void setup(){
  Serial.begin(115200);
  Wire.begin();
  initGyro();
}

void loop() {
  getGyroscopeData(gyro);
  Serial.print("@");
  Serial.print(gyro[0]);
  Serial.print(",");
  Serial.print(gyro[1]);
  Serial.print(",");
  Serial.print(gyro[2]);
  Serial.print("\n");
  delay(100);
}

void initGyro() {
  writeTo(GYRO_Address, G_PWR_MGM, 0x00);
  writeTo(GYRO_Address, G_SMPLRT_DIV, 0x07);
  writeTo(GYRO_Address, G_DLPF_FS, 0x1E);
  writeTo(GYRO_Address, G_INT_CFG, 0x00);
}

void getGyroscopeData(int * result) {
  int regAddress = 0x1D;
  int temp, x, y, z;
  byte buff[G_TO_READ];

  readFrom(GYRO_Address, regAddress, G_TO_READ, buff);
  result[0] = (buff[0] << 8) | buff[1];
  result[1] = (buff[2] << 8) | buff[3];
  result[2] = (buff[4] << 8) | buff[5];
}

void writeTo(int DEVICE, byte address, byte val){
  Wire.beginTransmission(DEVICE); //start transmission to ACC
  Wire.write(address); // send register address
  Wire.write(val); // send value to write
  Wire.endTransmission( ); //end transmission
}

void readFrom(int DEVICE, byte address, int num, byte buff[ ]){
  int k = 0;
  Wire.beginTransmission(DEVICE); //start transmission to ACC
  Wire.write(address); //sends address to read from
  Wire.endTransmission( ); //end transmission
  Wire.beginTransmission(DEVICE); //start transmission to ACC
  Wire.requestFrom(DEVICE, num); //request 6 bytes from ACC
  while( Wire.available( ) ) { //ACC may send less than requested (abnormal)
    buff[k] = Wire.read( ); //receive a byte
    k++;
  }
  Wire.endTransmission( ); //end transmission
}