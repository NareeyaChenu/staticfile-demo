import { useState, useEffect, useCallback } from 'react';
import Box from '@mui/material/Box';
import Typography from '@mui/material/Typography';
import axios from 'axios';
// import { useParams } from 'react-router-dom'
import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';
import CardMedia from '@mui/material/CardMedia';
import CardActionArea from '@mui/material/CardActionArea';


// Variables
const initialState = {
    blogs: [],
    isError: null,
    isLoading: false
};


function GalleryMain() {
    const [state, setState] = useState(initialState);

    // const params = useParams();
    // const id = params.id;


    const setBlogs = useCallback(callback => {
        setState(callback);
    }, [])

    const initBlos = () => {
        setBlogs(prev => ({
            ...prev,
            isLoading: true
        }))

        const params = {

        };

        axios.get(
            'https://localhost:5404/api/v1/blog'
        )
            .then(res => {
                if (res.status === 200) {
                    setBlogs(prev => ({
                        ...prev,
                        blogs: res.data
                    }))
                }

            })
            .catch(e => {
                setBlogs(prev => ({
                    ...prev,
                    isError: e.message
                }))
                // console.error('Get blogs error!', e);
            })
            .finally(f => {
                setBlogs(prev => ({
                    ...prev,
                    isLoading: false
                }))
            })
        // eslint-disable-next-line react-hooks/exhaustive-deps
    };

    useEffect(() => {

        initBlos();

    // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [])
    return (
        <>
            {state.isLoading && <p>Loading...</p>}
            <Box
                sx={{
                    display: 'flex',
                    flexDirection: 'column',
                    alignItems: 'center',
                    justifyContent: 'center',
                    maxWidth: '80%'
                }}
            >
                <Typography variant='h4'>
                    Blog Galleries
                </Typography>
                <Box className="text-center"
                    sx={{
                        display: 'flex',
                        flexDirection: 'row'
                    }}
                >
                    {state?.blogs?.map((blog, i) => {
                        return (
                            <Card
                                key={i}
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
                                        image={blog.ImageUrl}
                                        alt='image not found!'
                                    />
                                    <CardContent>
                                        <Typography gutterBottom variant="h5" component="div">
                                            {blog.Tiltle}
                                        </Typography>
                                        <Typography variant="body2" color="text.secondary">
                                            {blog.Description}
                                        </Typography>
                                    </CardContent>
                                </CardActionArea>
                            </Card>
                        )
                    })}
                </Box>
            </Box>
        </>
    )
}

export default GalleryMain