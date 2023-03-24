#define RAIN A0

void setup() {
  Serial.begin(9600);
  pinMode(RAIN, INPUT);
}

void loop() {
  Serial.print("Rain/Steam : ");
  Serial.println(analogRead(RAIN));
  delay(500);
}