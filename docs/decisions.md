# Technical Decisions

## User

- PhoneNumber is the primary identifier.
- Email is optional.

## Chat

- Supports Private and Group chats.
- Name is optional and mainly used for Group chats.

## Message

- Messages belong to a Chat.
- Messages do not contain a global status.

## ChatParticipant

- Stores participant role.
- Stores joined date.
- Stores last read message.