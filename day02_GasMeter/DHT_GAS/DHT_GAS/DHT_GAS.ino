#include <SimpleDHT.h>
int DHT_pin = 8;
SimpleDHT11 dht11(DHT_pin);

#define Packet_length 7
unsigned char TxData[Packet_length] = {'@', 'G', 0x00, 0x00, 0x00, 0x00, '\n'};

#define MQ5 A3
unsigned int GasValue;

byte temp = 0;
byte humi = 0;

void setup() {
  Serial.begin(9600);
  pinMode(MQ5, INPUT);
}

void loop() {
  int err = dht11.read(&temp, &humi, NULL);
  if(err != SimpleDHTErrSuccess) {
    Serial.print("Error");
    Serial.println(err);
    delay(1000);
    return;
  }

  Serial.print((int)temp); Serial.print(",");
  Serial.print((int)humi);
  
  GasValue = MQ5_ppm();
  TxPacket(GasValue);
  Serial.write(TxData,Packet_length);
  delay(500);
}

void TxPacket(int Value) {
  int temp = Value;
  TxData[2] = (temp / 1000) | 0x30;
  temp = temp % 1000;
  TxData[3] = (temp / 100) | 0x30;
  temp = temp % 100;
  TxData[4] = (temp / 10) | 0x30;
  TxData[5] = (temp % 10) | 0x30;
}

unsigned int MQ5_ppm() {
  unsigned int ADValue = analogRead(MQ5) / 0.6;
  float Ratio = ADValue / (614 - ADValue);
  if(Ratio > 6.7) Ratio = 6.7;
  unsigned int ppm = pow(2.71828182, 0.7201 * Ratio) * 119.37;
  if(ppm > 1000) ppm = 1000;
  return ppm;
}