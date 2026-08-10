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

## Domain Decisions

- Chat is the Aggregate Root.
- Chat owns Messages and ChatParticipants.
- Private chats support exactly two members.
- Group chats support participant roles (Owner, Admin, Member).
- Entity identifiers are generated inside the Domain using Guid.NewGuid().