#include <SPP.h>
#include <SPI.h>

USB usb;
BTD btd(&usb);

SPP SerialBT(&btd, "WOONG-BT", "1234");

bool firstMessage = true;

void setup() {
  Serial.begin(115200);

  if(usb.Init() == -1) {
    Serial.println(F("\r\nOSC did not start"));
    while (1);
  }
  Serial.println(F("\r\nSPP Bluetooth Library Started"));
}

void loop() {
  usb.Task();

  if(SerialBT.connected) {
    if(firstMessage) {
      firstMessage = false;
      SerialBT.println(F("Hello from Arduino"));

    }

    if(Serial.available()) {
      SerialBT.write(Serial.read());
    }
      
    if(SerialBT.available()) {
      Serial.write(SerialBT.read());
    }
      
    else {
      firstMessage = true;
    }
    
  }

}
