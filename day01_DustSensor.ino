#define OUT A2
#define LED A1

int DustADC = 0;
float dustDensity = 0;

void setup() {
  Serial.begin(115200);
  pinMode(LED, OUTPUT);
  pinMode(OUT, INPUT);
  digitalWrite(LED, HIGH);
}

void loop() {
  DustADC = SensorRead();
  Serial.print("Dust[ug/m3]:");
  dustDensity = DustDensity_ugPm3(DustADC);
  Serial.println(dustDensity);

  Serial.print("@");
  Serial.print("D");
  Serial.print((float)dustDensity);
  Serial.print("\n");

  delay(2000);
}

unsigned int SensorRead(void) {
  unsigned int Sensor_data;

  digitalWrite(LED, LOW);
  delayMicroseconds(280);
  Sensor_data = analogRead(OUT);
  delayMicroseconds(40);
  digitalWrite(LED, HIGH);
  delayMicroseconds(9680);
  return Sensor_data;
}

float DustDensity_ugPm3(int RawVal) {
  float Dust = (float) RawVal * (5.0 / 1023.0) / 5.8 - 0.1;
  return Dust * 1000;
}