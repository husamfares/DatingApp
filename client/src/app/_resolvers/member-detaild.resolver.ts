import { ResolveFn } from '@angular/router';
import { Member } from '../_models/member';
import { inject } from '@angular/core';
import { MembersService } from '../_service/members.service';

export const memberDetaildResolver: ResolveFn<Member | null> = (route, state) => {
  const memberService = inject(MembersService);

  const username = route.paramMap.get('username');
  if(!username) return null;


  return memberService.getMember(username);
};
