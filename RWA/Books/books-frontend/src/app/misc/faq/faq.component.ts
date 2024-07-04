import { Component } from '@angular/core';

@Component({
  selector: 'app-faq',
  templateUrl: './faq.component.html',
  styleUrl: './faq.component.scss',
})
export class FaqComponent {
  faqs = [
    {
      question: 'What is BooksArchive?',
      answer:
        'BooksArchive is a platform that allows users to archive, explore, and manage their book collections.',
    },
    {
      question: 'How do I create an account?',
      answer:
        'To create an account, click on the Sign Up button on the login page and fill in the required information.',
    },
    {
      question: 'How can I reset my password?',
      answer:
        'If you forgot your password, click on the Forgot Password link on the login page and follow the instructions to reset it.',
    },
    {
      question: 'How do I add a book to my collection?',
      answer:
        'To add a book to your collection, go to the Explore page, search for the book, and click on the Add to Collection button.',
    },
    {
      question: 'Can I share my book collection with others?',
      answer:
        'Yes, you can share your book collection with others by setting your collection to public in the settings.',
    },
  ];
}
