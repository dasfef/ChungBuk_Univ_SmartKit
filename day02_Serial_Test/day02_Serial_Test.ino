int Count = 0;

void setup() {
  Serial.begin(9600);
}

void loop() {
  Serial.println(Count);
  if(++Count > 9999) Count = 0;
  delay(250);
}
