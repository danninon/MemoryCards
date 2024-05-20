import Dropzone from 'react-dropzone'
import xlsxParser from 'xlsx-parse-json';

const CreateGroup = ({createGroup}) =>
    <Dropzone onDrop={async acceptedFiles => {
        try {
            const data = await xlsxParser.onFileSelection(acceptedFiles[0]);
            data.Sheet1.map(d => (d.groupName = acceptedFiles[0].name));
            createGroup(data.Sheet1);
            console.log('data:', data);
        } catch (error) {
            console.error('Error parsing file:', error);
        }
    }}>
        {({ getRootProps, getInputProps }) => (
            <section>
                <div {...getRootProps()}>
                    <input {...getInputProps()} />
                    <div className='drop-zone'>
                        Drop file here
                    </div>
                </div>
            </section>
        )}
    </Dropzone>


export default CreateGroup;