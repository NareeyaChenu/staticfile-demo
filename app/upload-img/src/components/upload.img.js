import { useState } from 'react';
import Box from '@mui/material/Box';
import Typography from '@mui/material/Typography';
import TextField from '@mui/material/TextField';
import Grid from '@mui/material/Grid';
import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';
import CardMedia from '@mui/material/CardMedia';
import CardActionArea from '@mui/material/CardActionArea';
import Button from '@mui/material/Button';
import axios from 'axios';

// icons
import AddRoundedIcon from '@mui/icons-material/AddRounded';

// Variables
const initialState = {
    img_title: "",
    img_description: "",
    name: "",
    files: [],
    set_errors: {
        create_error: false,
        create_error_message: ''
    },
    set_created: {
        create_status: false,
        create_message: ''
    }
};


export default function UploadImg() {
    const [state, setState] = useState(initialState);

    const getFiles = (files) => {
        setState(prev => ({
            ...prev,
            files: files
        }))
    }

    const handleChangeTitle = (e) => {
        setState(prev => ({
            ...prev,
            img_title: e.target.value
        }))
    };

    const handleChangeDesCription = (e) => {
        setState(prev => ({
            ...prev,
            img_description: e.target.value
        }))
    };

    const handleCreateBlog = (e) => {
        const body = {
            title: state.img_title,
            description: state?.img_description,
            file_data: state?.files[0]?.base64,
            file_type: state?.files[0]?.type,
        };
        console.log(body);
        axios.post(
            '',
            body
        )
            .then(res => {
                setState(prev => ({
                    ...prev,
                    set_created: {
                        ...prev.set_created,
                        create_status: true,
                        create_message: 'Create blog successfully'
                    }
                }))
            })
            .catch(e => {
                setState(prev => ({
                    ...prev,
                    set_errors: {
                        ...prev.set_errors,
                        create_error: true,
                        create_error_message: e.message
                    }
                }))
                console.error('There was an error!', e);
            })
    };

    return (
        <Box
            sx={{
                display: 'flex',
                flexDirection: 'column',
                alignItems: 'center',
                justifyContent: 'center'
            }}
        >

            <Typography className="text-center" variant='h4'>
                Create Blog With Upload Image to Base64 Converter
            </Typography>
            <Grid container spacing={2}>
                <Grid item xs={4}>
                    <Box
                        sx={{
                            mt: 5
                        }}
                    >
                        <Typography className="text-center" variant='h6'>
                            Complete
                        </Typography>
                        <Box
                            component="form"
                            sx={{
                                '& > :not(style)': { m: 1, width: '25ch' },
                                display: 'flex',
                                flexDirection: 'column'
                            }}
                            noValidate
                            autoComplete="off"
                        >
                            <TextField
                                id="img-title"
                                label="Image title"
                                value={state.img_title}
                                onChange={handleChangeTitle}
                            />
                            <TextField
                                id="img-description"
                                label="Image description"
                                value={state.img_description}
                                onChange={handleChangeDesCription}
                            />

                            <Box>
                                <Typography className="text-center" variant='subtitle1'>
                                    Try to upload some image~
                                </Typography>
                                <FileBase64
                                    multiple={true}
                                    onDone={getFiles} />
                            </Box>
                        </Box>
                        <Box>
                            <Button
                                variant="outlined"
                                startIcon={<AddRoundedIcon />}
                                onClick={handleCreateBlog}
                            >
                                Create
                            </Button>
                        </Box>
                    </Box>
                </Grid>
                <Grid item xs={8}>
                    <Box
                        sx={{
                            mt: 5
                        }}
                    >
                        <Typography className="text-center" variant='h6'>
                            Preview
                        </Typography>
                        <Box className="text-center"
                            sx={{
                                display: 'flex',
                                flexDirection: 'row'
                            }}
                        >
                            {state?.files?.map((file, i) => {
                                return (
                                    <Card
                                        sx={{
                                            maxWidth: 305,
                                            mr: 2
                                        }}
                                    >
                                        <CardActionArea>
                                            <CardMedia
                                                sx={{
                                                    width: '92%'
                                                }}
                                                component="img"
                                                height="250"
                                                image={file.base64}
                                                alt='image not found!'
                                            />
                                            <CardContent>
                                                <Typography gutterBottom variant="h5" component="div">
                                                    {state.img_title}
                                                </Typography>
                                                <Typography variant="body2" color="text.secondary">
                                                    {state.img_description}
                                                </Typography>
                                            </CardContent>
                                        </CardActionArea>
                                    </Card>
                                )
                            })}
                        </Box>

                    </Box>
                </Grid>
            </Grid>
            {state.files.length !== 0 ?
                <Box>
                    <Box className="text-center mt-25">Callback Object</Box>
                    <Box className="pre-container">
                        <pre>{JSON.stringify(state.files, null, 2)}</pre>
                    </Box>
                </Box>
                : null}
        </Box>
    )
}

const FileBase64 = (props) => {

    const handleChange = (e) => {

        // get the files
        let files = e.target.files;

        // Process each file
        var allFiles = [];
        for (var i = 0; i < files.length; i++) {

            let file = files[i];

            // Make new FileReader
            let reader = new FileReader();

            // Convert the file to base64 text
            reader.readAsDataURL(file);

            // on reader load somthing...
            reader.onload = () => {

                // Make a fileInfo Object
                let fileInfo = {
                    name: file.name,
                    type: file.type,
                    size: Math.round(file.size / 1000) + ' kB',
                    base64: reader.result,
                    file: file,
                };

                // Push it to the state
                allFiles.push(fileInfo);

                // If all files have been proceed
                if (allFiles.length === files.length) {
                    // Apply Callback function
                    if (props.multiple) props.onDone(allFiles);
                    else props.onDone(allFiles[0]);
                }
            }
        }

    }
    return (
        <input
            type="file"
            onChange={handleChange}
            multiple={props.multiple} />
    );
}

FileBase64.defaultProps = {
    multiple: false,
};