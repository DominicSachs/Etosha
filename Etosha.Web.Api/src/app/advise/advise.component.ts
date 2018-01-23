import { Component, OnInit } from '@angular/core';
import { AdviseService } from './advise.service';
import { error } from 'selenium-webdriver';

@Component({
  templateUrl: './advise.component.html',
  styleUrls: ['./advise.component.scss']
})
export class AdviseComponent implements OnInit {

  constructor(private adviseService: AdviseService) { }

  ngOnInit() {
    this.adviseService.getAdvice('C73B4DAA-C58A-4998-AD27-A72B48828413')
      .subscribe(
        data => {
          console.log(data);
        },
        e => {
          console.log(e);
        });
  }
}
