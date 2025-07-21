import { CommonModule } from '@angular/common';
import { Component, computed, Input, OnDestroy, OnInit, Signal, signal } from '@angular/core';
import { formatDistanceStrict, intervalToDuration, isBefore } from 'date-fns';

@Component({
  selector: 'app-countdown',
  imports: [CommonModule],
  templateUrl: './countdown.component.html',
  styleUrl: './countdown.component.scss'
})
export class CountdownComponent implements OnInit, OnDestroy {
  @Input() targetDate!: Date;
  @Input() message: string = 'Temps restat :';
  @Input() expiredMessage: string = "Offre expiréé.";

  private intervalId: any;
  now = signal(new Date());

  isExpired: Signal<boolean> = computed(() => isBefore(this.targetDate, this.now()));

  duration = computed(() =>
    intervalToDuration({ start: this.now(), end: this.targetDate })
  );

  ngOnInit(): void {
    this.intervalId = setInterval(() => {
      this.now.set(new Date());
    },1000);
  }
  ngOnDestroy(): void {
    clearInterval(this.intervalId);
  }

}
