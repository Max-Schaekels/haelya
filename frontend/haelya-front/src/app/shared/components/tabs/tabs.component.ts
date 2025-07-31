import { CommonModule } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-tabs',
  imports: [CommonModule],
  templateUrl: './tabs.component.html',
  styleUrl: './tabs.component.scss'
})
export class TabsComponent implements OnInit {

  activeTab: number = 0;

  @Input() tabs: { title: string; content: string }[] = [];

  setActiveTab(index: number) {
    this.activeTab = index;
  }

  ngOnInit(): void {
    if (this.tabs.length === 0) {
      this.tabs = [{ title: 'Aucun contenu', content: 'Aucun onglet fourni.' }];
    }
  }

}
