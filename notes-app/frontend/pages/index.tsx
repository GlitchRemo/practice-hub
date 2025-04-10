import React, { useMemo, useState, useCallback } from 'react';
import { createEditor, Descendant, BaseEditor, Transforms, Editor, Element as SlateElement } from 'slate';
import { Slate, Editable, withReact, ReactEditor } from 'slate-react';
import { withHistory, HistoryEditor } from 'slate-history';

type CustomText = { text: string };
type ParagraphElement = { type: 'paragraph'; children: CustomText[] };

declare module 'slate' {
  interface CustomTypes {
    Editor: BaseEditor & ReactEditor & HistoryEditor;
    Element: ParagraphElement;
    Text: CustomText;
  }
}

const toggleBullet = (editor: BaseEditor & ReactEditor & HistoryEditor) => {
  const [match] = Editor.nodes(editor, {
    match: n => SlateElement.isElement(n) && n.type === 'paragraph',
  });

  if (match) {
    const [node] = match;
    const isBulleted = node.children[0]?.text.startsWith('• ');

    Transforms.setNodes(
      editor,
      {
        children: [{ text: isBulleted ? node.children[0].text.slice(2) : `• ${node.children[0].text}` }],
      },
      { match: n => SlateElement.isElement(n) && n.type === 'paragraph' }
    );
  }
};

const Home: React.FC = () => {
  const editor = useMemo(() => withHistory(withReact(createEditor())), []);
  const [value, setValue] = useState<Descendant[]>([
    {
      type: 'paragraph',
      children: [{ text: 'Write your notes here...' }],
    },
  ]);

  const handleKeyDown = useCallback(
    (event: React.KeyboardEvent) => {
      if (event.metaKey && event.key === 'b') {
        event.preventDefault();
        toggleBullet(editor);
      }
    },
    [editor]
  );

  return (
      <div style={{ padding: '20px', height: '100vh', backgroundColor: '#f9f9f9' }}>
        <h1>Notes App</h1>
        <Slate editor={editor} initialValue={value} onChange={newValue => setValue(newValue)}>
          <Editable
            style={{
              border: '1px solid #ccc',
              borderRadius: '8px',
              padding: '10px',
              minHeight: '300px',
              backgroundColor: 'white',
            }}
            placeholder="Start typing..."
            spellCheck
            autoFocus
            onKeyDown={handleKeyDown}
          />
        </Slate>
      </div>
  );
};

export default Home;
