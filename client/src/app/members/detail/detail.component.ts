import { Component, inject, OnInit, ViewChild } from '@angular/core';
import { AccountService } from '../../_service/account.service';
import { Member } from '../../_models/member';
import { ActivatedRoute } from '@angular/router';
import { MembersService } from '../../_service/members.service';
import { TabDirective, TabsetComponent, TabsModule } from 'ngx-bootstrap/tabs';
import { GalleryItem, GalleryModule, ImageItem } from 'ng-gallery';
import { TimeagoModule } from 'ngx-timeago';
import { DatePipe } from '@angular/common';
import { MemberMessagesComponent } from "../member-messages/member-messages.component";
import { Message } from '../../_models/message';
import { MessageService } from '../../_service/message.service';

@Component({
  selector: 'app-detail',
  standalone: true,
  imports: [TabsModule, GalleryModule, TimeagoModule, DatePipe, MemberMessagesComponent],
  templateUrl: './detail.component.html',
  styleUrl: './detail.component.css'
})
export class DetailComponent implements OnInit{
@ViewChild('memberTabs' , {static : true}) memberTabs?:TabsetComponent;
private memberService = inject(MembersService);
 private route = inject(ActivatedRoute);
private messageService = inject(MessageService);
member: Member = {} as Member;
images: GalleryItem[] = [];
activeTab?: TabDirective;
messages: Message[] =[];

ngOnInit(): void {
  this.route.data.subscribe({
    next: data => {
      this.member = data['member'];
      this.member && this.member.photos.map(p => {
        this.images.push(new ImageItem({src :p.url , thumb: p.url}))
      })
    }
  })

  

  this.route.queryParams.subscribe({
    next: params => {
      params['tab'] && this.selectTab(params['tab'])
    }
  })
}

onUpdateMessage(event: Message)
{
  this.messages.push(event);
}

selectTab(heading: string)
{
  if(this.memberTabs)
  {
    const messageTab = this.memberTabs.tabs.find(x => x.heading === heading)

    if(messageTab) messageTab.active = true;
  }
}

onTabActivated(data: TabDirective)
{
  this.activeTab = data;
  if(this.activeTab.heading === 'Messages'  && this.messages.length === 0 && this.member)
  {
    this.messageService.getMessageThread(this.member.name).subscribe({
      next : messages => this.messages = messages
    })
  }
}

// loadMember()
// {
//   const username =  this.route.snapshot.paramMap.get('username');
//   if(!username) return;
//   this.memberService.getMember(username).subscribe({
//     next : member => 
//       {this.member = member
//         member.photos.map(p => {
//           this.images.push(new ImageItem({src :p.url , thumb: p.url}))
//         })

//     }
//   })

// }

}
