import { Photo } from "./Photo"

export interface Member {
    id: number
    name: string
    dataOfBirth: string
    age: number
    photoUrl: string
    knownAs: string
    created: Date
    lastActive: Date
    gender: string
    introduction: string
    intrests: any
    lookingFor: string
    city: string
    country: string
    photos: Photo[]
  }
  
  