import { Component, Input } from '@angular/core';
import { MoveRecord } from '../../models/game.model';

@Component({
  selector: 'app-move-history',
  standalone: false,
  templateUrl: './move-histoy.component.html',
  styleUrls: ['./move-histoy.component.css']
})
export class MoveHistoyComponent {

  @Input() moves: MoveRecord[] = [];

}
