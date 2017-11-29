import { Component } from '@angular/core';

@Component({
  selector: 'hello-world',
  template: `
    <h1>Hello, World!</h1>
    <p>This is an Angular 4 component :)</p>
    <p>This next thing is an upgraded v1 component:</p>
    <div style="background-color:#eee; padding:0 1rem;">
      <main-top-nav></main-top-nav>
    </div>
  `
})
export class HelloWorldComponent {}
