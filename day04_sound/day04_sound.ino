#define Sound_PIN A1
unsigned int sound;
signed int prevSound;
signed int diffSound;

unsigned long sampleMillis = 100;
unsigned long startMillis;
unsigned long nowMillis;

unsigned int signalMax = 0;
unsigned int signalMin = 1024;

void setup() {
  Serial.begin(115200);
  pinMode(Sound_PIN, INPUT);
  startMillis = millis();
  prevSound = 0;
}

void loop() {
  nowMillis = millis();

  if(nowMillis - startMillis >= sampleMillis) {
    sound = analogRead(Sound_PIN);
    diffSound = sound - prevSound;
    prevSound = sound;
    if(sound > signalMax) signalMax = sound;
    if(sound < signalMin) signalMin = sound;
    startMillis = nowMillis;

    Serial.print(sound);
    Serial.print(":");
    Serial.print(diffSound);
    Serial.print(",");
    Serial.print(signalMin);
    Serial.print(",");
    Serial.println(signalMax);
  }
}