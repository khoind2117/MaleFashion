import React from 'react'
import { Accordion, AccordionDetails, AccordionSummary, Typography } from '@mui/material'
import ExpandMoreIcon from "@mui/icons-material/ExpandMore";

interface AccordionItemProps {
    title: string,
    children: React.ReactNode,
    id: string
}

const AccordionItem: React.FC<AccordionItemProps> = ({title, children, id}) => {
  return (
    <Accordion defaultExpanded>
        <AccordionSummary
            expandIcon={<ExpandMoreIcon/>}
            aria-controls={`${id}-content`}
            id={`${id}-header`}
        >
            <Typography style={{ textTransform: "uppercase", fontWeight: 'bold' }}>{title}</Typography>
        </AccordionSummary>
        <AccordionDetails>
            {children}
        </AccordionDetails>
    </Accordion>
  )
}

export default AccordionItem 