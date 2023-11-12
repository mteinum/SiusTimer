/*
   SIUS TIMER COMPETITION HELPER

   morten.teinum@gmail.com

   Hardware:
   Arduino Micro Board
   
   */

#include "Keyboard.h"

int relay = 6;

// program state

void setup() {
  Serial.begin(9600);

  pinMode(LED_BUILTIN, OUTPUT);
  pinMode(relay, OUTPUT);

  // initialize control over the keyboard:
  Keyboard.begin();
}

void beepOn() {
  digitalWrite(LED_BUILTIN, HIGH);
  digitalWrite(relay, HIGH);
}

void beepOff() {
  digitalWrite(LED_BUILTIN, LOW);
  digitalWrite(relay, LOW);
}


void loop() {

  if (Serial.available() > 0) {

    String command = Serial.readString();

    if (command == "on") {
      beepOn();
    }
    else if (command == "off") {
      beepOff();
    }
    else {
      // error light?
    }
  }

}
