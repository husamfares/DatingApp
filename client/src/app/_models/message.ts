export interface Message {
    id: number
    senderId: number
    sendUserName: string
    senderPhotoUrl: string
    recipientId: number
    recipientUserName: string
    recipientPhotoUrl: string
    content: string
    dateRead?: Date
    messageSent: Date
  }
  