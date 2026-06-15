import { Component, Input } from '@angular/core';
import { Scoreboard } from '../../models/game.model';

@Component({
  selector: 'app-scoreboard',
  standalone: false,
  templateUrl: './scoreboard.component.html',
  styleUrls: ['./scoreboard.component.css']
})
export class ScoreboardComponent {
  @Input() scoreboard: Scoreboard | null = null;

}
